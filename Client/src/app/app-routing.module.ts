import { NotFoundComponent } from './core/not-found/not-found.component';
import { ProductDetailsComponent } from './shop/product-details/product-details.component';
import { HomeComponent } from './home/home.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ShopComponent } from './shop/shop.component';

const routes: Routes = [
  {path : '', component : HomeComponent, data:{breadcrumb:"صفحه اصلی"}},
  {path : 'not-found', component : NotFoundComponent, data:{breadcrumb:"سرویس مورد نظر یافت نشد"}},
  {path : 'server-error', component : NotFoundComponent, data:{breadcrumb:"خطا سرور"}},
  {path:'shop', loadChildren: () => import("./shop/shop.module").then(mod => mod.ShopModule), data:{breadcrumb:"محصولات"}},
  {path:'basket', loadChildren: () => import("./basket/basket.module").then(mod => mod.BasketModule), data:{breadcrumb:"سبد خرید"}},
  {path:'**', redirectTo: 'not-found', pathMatch : 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
