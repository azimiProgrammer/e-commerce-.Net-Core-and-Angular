import { IProduct } from './models/product';
import { IPagination } from './models/pagination';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'ecummers';
  products: IProduct[] = [];
  constructor(private http: HttpClient){
    
  }

  ngOnInit(): void {
    this.http.get<IPagination<IProduct>>("https://localhost:5001/api/Product").subscribe((response : IPagination<IProduct>) =>{
      this.products = response.data;
    },
    error => {
      console.log(error)
    } )
  }
}
