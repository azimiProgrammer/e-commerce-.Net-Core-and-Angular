import { IBasketTotals } from './../shared/models/basket';
import { IProduct } from 'src/app/shared/models/product';
import { IBasket, IBasketItem } from 'src/app/shared/models/basket';
import { map } from 'rxjs/Operators';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Basket} from 'src/app/shared/models/basket';


@Injectable({
  providedIn: 'root'
})
export class BasketService {

  baseUrl: string = environment.baseUrl
  private basketSource = new BehaviorSubject<IBasket>({id:"0", items:[]});
  basket$ = this.basketSource.asObservable();
  private basketTotalSource = new BehaviorSubject<IBasketTotals>({shipping:0,subTotal:0,total:0});
  basketTotal$ = this.basketTotalSource.asObservable()

  constructor(private http: HttpClient) {


   }

   getBasket(id: string){
     return this.http.get<IBasket>(this.baseUrl + 'Basket?id=' + id)
      .pipe(
        map((basket: IBasket) => {
          this.basketSource.next(basket);
          this.calculateTotals();
        })
      );
   }

   setBasket(basket: IBasket){
     return this.http.post<IBasket>(this.baseUrl + 'Basket', basket).subscribe((response: IBasket) =>{
       this.basketSource.next(response);
       this.calculateTotals();
     }, 
     error => {
       console.log('occured error set basket')
       console.log(error)
      })
   }

   deleteBasket(basket: IBasket) {
    return this.http.delete<IBasket>(this.baseUrl + 'Basket?id=' + basket.id).subscribe((response: IBasket) =>{
      this.basketSource.next({id:"0", items:[]});
      this.basketTotalSource.next({shipping:0,subTotal:0,total:0})
      localStorage.removeItem('basket_id');
    }, 
    error => {
      console.log('occured error delete basket')
      console.log(error)
     })
  }

   getCurrnetBasketValue(){
     return this.basketSource.value;
   }

   addItemBasket(item: IProduct, quantity = 1){
     const itemToAdd: IBasketItem = this.mapProductToBasketItem(item, quantity);
     const basket: IBasket = this.getCurrnetBasketValue().id === "0" ? this.createBasket() : this.getCurrnetBasketValue();
     basket.items = this.addOrUpdateItems(basket.items, itemToAdd, quantity);
     this.setBasket(basket);
   }

   incerementItemQuantity(item: IBasketItem){
     const basket = this.getCurrnetBasketValue();
     const findItemdIndex = basket.items.findIndex(i => i.id === item.id);
     basket.items[findItemdIndex].quantity++;
     this.setBasket(basket);
   }

   decerementItemQuantity(item: IBasketItem){
    const basket = this.getCurrnetBasketValue();
    const findItemdIndex = basket.items.findIndex(i => i.id === item.id);
    if(basket.items[findItemdIndex].quantity > 1)
    {
      basket.items[findItemdIndex].quantity--;
      this.setBasket(basket);
    }
    else{
      this.removeItemFromBasket(item)
    }
    
  }
  removeItemFromBasket(item: IBasketItem) {
    const basket = this.getCurrnetBasketValue();
    if(basket.items.some(i => i.id === item.id)){
      basket.items = basket.items.filter(i => i.id !== item.id)
      if(basket.items.length > 0){
        this.setBasket(basket);
      }
      else{
        this.deleteBasket(basket);
      }
    }
  }


  

  
  private calculateTotals(){
    const basket = this.getCurrnetBasketValue();
    const shipping = 0;
    const subTotal = basket.items.reduce((a , b) => (b.quantity * b.price) + a , 0)
    const total = subTotal + shipping;

    this.basketTotalSource.next({shipping, total, subTotal})
  }

  
  private addOrUpdateItems(items: IBasketItem[], itemToAdd: IBasketItem, quantity: number): IBasketItem[] {
    const index = items.findIndex(i => i.id === itemToAdd.id);
    if(index === -1)
    {
      itemToAdd.quantity = quantity;
      items.push(itemToAdd);
    }
    else
      items[index].quantity += quantity;
    return items;
  }

  private createBasket(): IBasket {
    const basket = new Basket();
    localStorage.setItem('basket_id', basket.id)
    return basket;
  }
  private mapProductToBasketItem(product: IProduct, quantity: number) : IBasketItem {
    return {
      id: product.id,
      productName: product.name,
      pictureUrl: product.imageUrl,
      price: product.price,
      quantity: quantity,
      brand: product.productBrandName,
      type: product.productTypeName
    }
  }
}
