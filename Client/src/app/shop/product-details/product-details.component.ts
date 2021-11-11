import { IProduct } from 'src/app/shared/models/product';
import { ShopService } from './../shop.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product! : IProduct;

  constructor(private shopService: ShopService, private ActivateRoute: ActivatedRoute, private bcService: BreadcrumbService ) { }

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct(){
    this.shopService.getProduct(+this.ActivateRoute.snapshot.paramMap.get('id')!)
    .subscribe(product =>{
      this.product = product;
      this.bcService.set('@productDetails', product.name);
    },
      error =>{
        console.log("error in get product")
      } 
    )
  }
}
