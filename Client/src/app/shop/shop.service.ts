import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IProductBrand } from '../shared/models/productBrand';
import { IProductType } from '../shared/models/productType';
import { IPagination } from './../shared/models/pagination';
import { IProduct } from './../shared/models/product';
import {map} from 'rxjs/Operators';
import { ShopParams } from '../shared/models/shopParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl : string = "https://localhost:5001/api/"
  constructor(private http : HttpClient) { }

  public getProducts(shopParams : ShopParams){
    let params = new HttpParams();

    if(shopParams.brandId !== 0)
      params = params.append("brandId", shopParams.brandId.toString());
    
    if(shopParams.typeId !== 0)
      params = params.append("typeId", shopParams.typeId.toString());

    params = params.append("sort", shopParams.sort);

    params = params.append("pageNumber", shopParams.pageNumber.toString())
    params = params.append("pageSize", shopParams.pageSize.toString())

    return this.http.get<IPagination<IProduct>>(this.baseUrl + "product", {observe : 'response', params})
      .pipe(
        map(response =>{
          return response.body;
        })
      );
  }

  public getProductBrands(){
    return this.http.get<IProductBrand[]>(this.baseUrl + "ProductBrand");
  }

  public getProductTypes(){
    return this.http.get<IProductType[]>(this.baseUrl + "ProductType");
  }
}
