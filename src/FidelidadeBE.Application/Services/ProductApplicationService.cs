using AutoMapper;
using FidelidadeBE.Application.Interfaces;
using FidelidadeBE.Business.Entities;
using FidelidadeBE.Business.Entities.Validations;
using FidelidadeBE.Business.Interfaces;
using FidelidadeBE.Business.Models.Product;
using FidelidadeBE.Core.Interfaces;
using FidelidadeBE.Core.Notifications;
using FidelidadeBE.Data.Interfaces;

namespace FidelidadeBE.Application.Services;

public class ProductApplicationService : IProductApplicationService
{
    private readonly INotificator _notificator;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICategory_SubCategoryRepository _categorySubCategoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDomainBaseService _domainBaseService;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductApplicationService(
        INotificator notificator,
        ICategoryRepository categoryRepository,
        ICategory_SubCategoryRepository categorySubCategoryRepository,
        IUnitOfWork unitOfWork,
        IDomainBaseService domainBaseService,
        IProductRepository productRepository,
        IMapper mapper
    )
    {
        _notificator = notificator;
        _categoryRepository = categoryRepository;
        _categorySubCategoryRepository = categorySubCategoryRepository;
        _unitOfWork = unitOfWork;
        _domainBaseService = domainBaseService;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<AddProductResponseModel?> AddProductAsync(AddProductRequestModel product)
    {
        var categories = product.CategoryPath.Split('/');
        var productCategories = new List<Category>();

        if (categories is {Length: 0})
        {
            _notificator.AddNotification("Any category was informed", NotificationType.IncorrectData);
            return null;
        }

        for (var categoryIndex = 0; categoryIndex < categories.Length && categories[categoryIndex] != ""; categoryIndex++)
            if (categoryIndex == 0)
            {
                var foundCategory = await ObtainCategoryAsync(categories[categoryIndex], null, categoryIndex + 1);

                if (foundCategory == null)
                    return null;

                productCategories.Add(foundCategory);
            }
            else
            {
                var foundCategory = await ObtainCategoryAsync(categories[categoryIndex],
                    categories[categoryIndex - 1], categoryIndex + 1);

                if (foundCategory == null)
                    return null;

                productCategories.Add(foundCategory);

                if (!await AddSubCategoryRelationsAsync(productCategories[^1].Id,
                        productCategories[^2].Id)) return null;
            }

        var newProduct = new Product(product.Name, product.Points, productCategories[^1]);

        if (!_domainBaseService.IsEntityValid(new ProductValidation(), newProduct)) return null;

        await _productRepository.AddAsync(newProduct);

        if (await _unitOfWork.CommitAsync()) return _mapper.Map<AddProductResponseModel>(newProduct);

        _notificator.AddNotification("Any changes were done at the database", NotificationType.IncorrectData);
        return null;
    }

    public async Task<IEnumerable<AddProductResponseModel>?> GetAvailableProductsAsync()
    {
        var products = await _productRepository.GetManyAsync(x => x.Point == null, true);

        return !products.Any() ? null : _mapper.Map<IEnumerable<AddProductResponseModel>>(products);
    }

    private async Task<Category?> ObtainCategoryAsync(string subCategoryName,
        string? parentCategoryName,
        int categoryLevel)
    {
        if (parentCategoryName == null)
        {
            var category =
                await _categoryRepository.GetAsync(x => x.Level == categoryLevel && x.Name == subCategoryName, true);

            if (category != null) return category;

            var newCategory = new Category(subCategoryName, categoryLevel);

            if (!_domainBaseService.IsEntityValid(new CategoryValidation(), newCategory)) return null;

            await _categoryRepository.AddAsync(newCategory);
            return newCategory;
        }

        var subCategory = await _categorySubCategoryRepository.GetAsync(x => x.SubCategory!.Name == subCategoryName &&
            x.SubCategory.Level == categoryLevel &&
            x.ParentCategory!.Name == parentCategoryName &&
            x.ParentCategory.Level == categoryLevel - 1, true);

        if (subCategory != null) return subCategory.SubCategory;

        {
            var newCategory = new Category(subCategoryName, categoryLevel);

            if (!_domainBaseService.IsEntityValid(new CategoryValidation(), newCategory)) return null;

            await _categoryRepository.AddAsync(newCategory);
            return newCategory;
        }
    }

    private async Task<bool> AddSubCategoryRelationsAsync(Guid subCategoryId, Guid parentCategoryId)
    {
        var subCategoryFound = await _categorySubCategoryRepository.GetAsync(x =>
            x.SubCategoryId == subCategoryId && x.ParentCategoryId == parentCategoryId, true);

        if (subCategoryFound != null) return true;

        var subCategory = new Category_SubCategory(subCategoryId, parentCategoryId);

        if (!_domainBaseService.IsEntityValid(new Category_SubCategoryValidation(), subCategory))
            return false;

        await _categorySubCategoryRepository.AddAsync(subCategory);

        return true;
    }
}