import { NotFoundComponent } from './core/not-found/not-found.component';
import { ProductDetailsComponent } from './shop/product-details/product-details.component';
import { HomeComponent } from './home/home.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ShopComponent } from './shop/shop.component';

const routes: Routes = [
  {path : '', component : HomeComponent},
  {path : 'not-found', component : NotFoundComponent},
  {path : 'server-error', component : NotFoundComponent},
  {path : '', component : HomeComponent},
  {path:'shop', loadChildren: () => import("./shop/shop.module").then(mod => mod.ShopModule)},
  {path:'**', redirectTo: '', pathMatch : 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
