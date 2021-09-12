import { ShopParams } from './../shared/models/shopParams';
import { IProductBrand } from './../shared/models/productBrand';
import { ShopService } from './shop.service';
import { IProduct } from './../shared/models/product';
import { Component, OnInit } from '@angular/core';
import { IProductType } from '../shared/models/productType';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {

  products? : IProduct[] = [];
  brands : IProductBrand[] = [];
  types : IProductType[] = [];
  totalCount! : number;
  
  shopParams = new ShopParams();

  sortOptions = [
    {name : "حروف الفبا", value : "name"},
    {name : "ارزان ترین", value : "price"},
    {name : "گران ترین", value : "price desc"}
  ];

  constructor(private shopService : ShopService) { }

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts(){
    this.shopService.getProducts(this.shopParams)
    .subscribe(
      (response) =>{
        this.products = response?.data;
        this.shopParams.pageNumber = response!.pageNumber;
        this.shopParams.pageSize = response!.pageSize;
        this.totalCount = response!.count
      },
      error  => {
        console.log(error);
      })
  }

  getBrands(){
    this.shopService.getProductBrands().subscribe((response =>{
      this.brands = [{id : 0, name : "همه"}, ...response];
    }),
    error =>{
      console.log(error)
    })
  }

  getTypes(){
    this.shopService.getProductTypes().subscribe((response => {
      this.types = [{id : 0, name : "همه"}, ...response];
    }),
    error => {
      console.log(error);
    })
  }

  onBrandSelected(brandId: number){
    this.shopParams.brandId = brandId;
    this.getProducts();
  }

  onTypeSelected(typeId: number){
    this.shopParams.typeId = typeId;
    this.getProducts();
  }

  onSortSelected(sort: string){
    this.shopParams.sort = sort;
    this.getProducts();
  }

  onPageChanged(event:any){
    this.shopParams.pageNumber = event;
    this.getProducts();
  }


}
