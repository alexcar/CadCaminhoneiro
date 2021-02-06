import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { MarcaCaminhaoComponent } from './marca-caminhao/marca-caminhao.component';
import { MarcaCaminhaoEditarComponent } from './marca-caminhao/marca-caminhao-editar-component/marca-caminhao-editar.component';

const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'marcasCaminhao', component: MarcaCaminhaoComponent },
  { path: 'marcaCaminhao/:id', component: MarcaCaminhaoEditarComponent },
  { path: 'marcaCaminhao', component: MarcaCaminhaoEditarComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
