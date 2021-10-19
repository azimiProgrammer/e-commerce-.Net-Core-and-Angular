import { IProduct } from 'src/app/shared/models/product';
import { ShopService } from './../shop.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product! : IProduct;

  constructor(private shopService: ShopService, private ActivateRoute: ActivatedRoute ) { }

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct(){
    this.shopService.getProduct(+this.ActivateRoute.snapshot.paramMap.get('id')!)
    .subscribe(product =>{
      this.product = product
    },
      error =>{
        console.log("error in get product")
      } 
    )
  }
}
