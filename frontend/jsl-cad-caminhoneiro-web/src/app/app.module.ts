import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AngularMaterialModule } from './angular-material/angular-material.module';
import { FormControl, ReactiveFormsModule } from '@angular/forms';

import { CommonModule } from '@angular/common';
import { ToastrModule } from 'ngx-toastr';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { MarcaCaminhaoComponent } from './marca-caminhao/marca-caminhao.component';
import { MarcaCaminhaoEditarComponent } from './marca-caminhao/marca-caminhao-editar-component/marca-caminhao-editar.component';
import { ModeloCaminhaoComponent } from './modelo-caminhao/modelo-caminhao.component';
import { ModeloCaminhaoEditarComponent } from './modelo-caminhao/modelo-caminhao-editar/modelo-caminhao-editar.component';
import { MotoristaComponent } from './motorista/motorista.component';
import { MotoristaEditarComponent } from './motorista/motorista-editar/motorista-editar.component';

export function getBaseUrl() {
  return 'https://localhost:44313/api/';
}

@NgModule({
  declarations: [
    AppComponent,
    MarcaCaminhaoComponent,
    NavMenuComponent,
    HomeComponent,
    MarcaCaminhaoEditarComponent,
    ModeloCaminhaoComponent,
    ModeloCaminhaoEditarComponent,
    MotoristaComponent,
    MotoristaEditarComponent
   ],
  imports: [
    CommonModule,
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
    BrowserAnimationsModule,
    AngularMaterialModule,
    ReactiveFormsModule,
    ToastrModule.forRoot({
      timeOut: 10000
    }),
  ],
  providers: [
    { provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
