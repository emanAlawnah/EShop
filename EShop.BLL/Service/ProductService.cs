using EShop.DAL.DTO.Request;
using EShop.DAL.DTO.Response;
using EShop.DAL.Models;
using EShop.DAL.Repository;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _ProductRepository;
        private readonly IFileService _FileService;

        public ProductService(IProductRepository productRepository,IFileService fileService)
        {
            _ProductRepository = productRepository;
            _FileService = fileService;
        }
        public async Task<List<ProductResponse>> GetAllProductsForAdmin()
        {
            var products = await _ProductRepository.GetAllAsync();
            var response = products.Adapt<List<ProductResponse>>();
            return response;
        }

        public async Task<ProductResponse> CreateProduct(ProductRequest request)
        {
            var product = request.Adapt<Product>();

            if (request.MainImage != null) 
            { 
            var imagePath=await _FileService.UploadAsync(request.MainImage);
                product.MainImage = imagePath;
            }
            if (request.SubImages != null) { 
                product.SubImages=new List<ProductImage>();
                foreach (var file in request.SubImages) 
                { 
                var imagePath = await _FileService.UploadAsync(file);   
                product.SubImages.Add((new ProductImage
                {
                  ImageName=imagePath
                }
                    ));
                }
            }
            await _ProductRepository.AddAsync(product);
            return product.Adapt<ProductResponse>();
            
        }
    }
}
