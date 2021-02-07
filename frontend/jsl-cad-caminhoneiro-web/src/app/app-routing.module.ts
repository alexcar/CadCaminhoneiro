import { MotoristaEditarComponent } from './motorista/motorista-editar/motorista-editar.component';
import { MotoristaComponent } from './motorista/motorista.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { MarcaCaminhaoComponent } from './marca-caminhao/marca-caminhao.component';
import { MarcaCaminhaoEditarComponent } from './marca-caminhao/marca-caminhao-editar-component/marca-caminhao-editar.component';
import { ModeloCaminhaoComponent } from './modelo-caminhao/modelo-caminhao.component';
import { ModeloCaminhaoEditarComponent } from './modelo-caminhao/modelo-caminhao-editar/modelo-caminhao-editar.component';

const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'marcasCaminhao', component: MarcaCaminhaoComponent },
  { path: 'marcaCaminhao/:id', component: MarcaCaminhaoEditarComponent },
  { path: 'marcaCaminhao', component: MarcaCaminhaoEditarComponent },
  { path: 'modelosCaminhao', component: ModeloCaminhaoComponent },
  { path: 'modeloCaminhao/:id', component: ModeloCaminhaoEditarComponent },
  { path: 'modeloCaminhao', component: ModeloCaminhaoEditarComponent },
  { path: 'motoristas', component: MotoristaComponent },
  { path: 'motorista/:id', component: MotoristaEditarComponent },
  { path: 'motorista', component: MotoristaEditarComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
