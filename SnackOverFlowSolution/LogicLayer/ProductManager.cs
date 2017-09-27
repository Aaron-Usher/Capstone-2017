﻿using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class ProductManager : IProductManager
    {

        /// <summary>
        /// Created by Michael Takrama
        /// Created on 2017-02-10
        /// 
        /// Creates a new Product
        /// </summary>
        /// <param name="product">Product Object to be created</param>
        /// <returns>Success integer</returns>
        public int CreateProduct(Product product)
        {

            int result = 0;

            result = ProductAccessor.CreateProductAndRetrieveProductId(product);

            return result;

        }

        /// <summary>
        /// Mason Allen
        /// Created on 5/2/17
        /// Creates a new product record and returns the generated product id
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public int CreateProductReturnProductId(Product product)
        {
            int productId = 0;
            try
            {
                productId = ProductAccessor.CreateProductAndRetrieveProductId(product);
            }
            catch (Exception)
            {
                throw;
            }
            return productId;
        }



        ///<summary> 
        ///Dan Brown
        ///Created on 2017/03/10
        ///
        /// Delete an individual product from the product table (following documentation guidlines)
        ///</summary>
        ///<param name="productID"> The ID field of the product to be deleted </param>
        ///<returns> Returns rows affected (int) </returns>
        ///<exception cref="System.ApplicationException"> Thrown if 'ProductAccessor.DeleteProduct' returns 0 rows affected </exception>
        ///<exception cref="System.Exception"> Thrown if there is an error connecting to the 'ProductAccessor' class </exception>
        public int DeleteProduct(int productID)
        {
            int result = 0;

            try
            {
                if (1 == ProductAccessor.DeleteProduct(productID))
                {
                    result = 1;
                }
                else
                {
                    throw new ApplicationException("Product matching that productID was not found. No Product deleted");
                }
            }
            catch (Exception)
            {

                throw;
            }


            return result;
        }

        /// <summary>
        /// Redudant with RetrieveProducts; should be removed entirely.
        /// </summary>
        /// <returns></returns>
        public List<Product> ListProducts()
        {

            List<Product> products = null;
            try
            {

                products = ProductAccessor.RetrieveProductList();

            }
            catch (SqlException)
            {
                throw;
            }

            return products;
        }

        /// <summary>
        /// Laura Simmonds
        /// Created on 2017/02/20
        /// 
        /// Retrieves a product from the database
        /// </summary>
        /// <param name="productID"></param>
        /// <returns>product</returns>
        public Product RetrieveProductById(int productId)
        {
            Product product = null;

            try
            {
                product = ProductAccessor.RetrieveProduct(productId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem retrieving the product details.", ex);
            }
            return product;
        }

        public Product RetrieveProductByName(string name)
        {
            Product product = null;
            try
            {
                ProductAccessor.RetrieveProductByName();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There were a problem retrieving the product details", ex);
            }
            return product;
        }

        /// <summary>
        /// Created by Natacha Ilunga
        /// Created on 2/10/2017
        /// 
        /// Retrieves Product List
        /// </summary>
        /// <returns></returns>
        public List<BrowseProductViewModel> RetrieveProductsToBrowseProducts()
        {

            var productList = ProductAccessor.RetrieveProductsToBrowseProducts();

            return productList;
        }

        /// <summary>
        /// Created by Natacha Ilunga 
        /// Created on 2/10/2017
        /// 
        /// Filters Products From Presentation Layer
        /// </summary>
        /// <param name="vendors">Vendor Filters</param>
        /// <param name="categories">Category Filters</param>
        /// <param name="min">Price Minimum Value</param>
        /// <param name="max">Price Maximum Value</param>
        /// <returns>List of Filterd Products</returns>
        public List<BrowseProductViewModel> FilterProducts(List<string> vendors, List<string> categories, double min, double max)
        {

            var productsInDb = RetrieveProductsToBrowseProducts();

            var vendorFiltered = FilterByVendor(productsInDb, vendors);

            var categoryFiltered = FilterByCategory(vendorFiltered, categories);

            var priceFiltered = FilterByPrice(categoryFiltered, min, max);

            return priceFiltered;

        }

        /// <summary>
        /// Create by Natacha Ilunga 
        /// Creatd on 2/10/2017
        /// 
        /// Filters Products by Vendor/Supplier
        /// </summary>
        /// <param name="productsToFilter">Products to be filtered</param>
        /// <param name="vendors">Vendor filter criteria</param>
        /// <returns></returns>
        private List<BrowseProductViewModel> FilterByVendor(List<BrowseProductViewModel> productsToFilter, List<String> vendors)
        {
            List<BrowseProductViewModel> filteredProducts = null;
            if (vendors.Any())
            {
                filteredProducts = productsToFilter.Where(unfiltered => vendors.Any(vendor => vendor.Equals(unfiltered.Supplier_Name))).ToList();
            }
                
            return filteredProducts;
        }

        /// <summary>
        /// Created by Natacha Ilunga
        /// Created on 2/10/2017
        /// 
        /// Filters Products By Price
        /// </summary>
        /// <param name="productsToFilter">Products to be filtered</param>
        /// <param name="min">Minimum Price</param>
        /// <param name="max">Maximum Price</param>
        /// <returns></returns>
        private List<BrowseProductViewModel> FilterByPrice(List<BrowseProductViewModel> productsToFilter, double min, double max)
        {
            if (min != 0 && max != 0)
                return productsToFilter.Where(unfiltered => min <= unfiltered.Price && unfiltered.Price <= max).ToList();
            return productsToFilter;
        }

        /// <summary>
        /// Created by Natacha Ilunga 
        /// Created on 2/10/2017
        /// 
        /// Filters Products By Product Category
        /// </summary>
        /// <param name="productsToFilter">Products to be filtered</param>
        /// <param name="categories">Filter Categories</param>
        /// <returns>List of Filtered Productsby Category</returns>
        private List<BrowseProductViewModel> FilterByCategory(List<BrowseProductViewModel> productsToFilter, List<string> categories)
        {
            if (categories.Any())
                return productsToFilter.Where(unfiltered => categories.Any(category => category.Equals(unfiltered.CategoryID))).ToList();
            return productsToFilter;
        }

        /// <summary>
        /// Christian Lopez
        /// Created 2017/03/08
        /// 
        /// Tries to get a list of products from the accessor
        /// </summary>
        /// <returns></returns>
        public List<Product> RetrieveProducts()
        {
            try
            {
                return ProductAccessor.RetrieveProductList();
            }
            catch (SqlException ex)
            {

                throw new ApplicationException("There was a database error.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an unknown error.", ex);
            }
        }

        /// <summary>
        /// Natacha Ilunga
        /// Created: 03/29/2017
        /// 
        /// Retrieves Products by supplier id to view model
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 04/06/2017
        /// 
        /// Added very basic exception handling logic.
        /// </remarks>
        /// 
        /// <param name="id"></param>
        /// <returns></returns>
        public List<BrowseProductViewModel> RetrieveProductsBySupplierId(int id)
        {
            try
            {
                return ProductAccessor.RetrieveProductsBySupplierIdToViewModel(id);
            }
            catch(Exception)
            {
                throw;
            }
            
        }

        /// <summary>
        /// William Flood
        /// Created 2017/04/14
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public List<ProductGradePrice> RetrieveGradeForProduct(int productId)
        {
            try
            {
                return ProductAccessor.RetrievePriceOptionsForProduct(productId);
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("An error occured: ", ex);
            }
        }
    }
}
