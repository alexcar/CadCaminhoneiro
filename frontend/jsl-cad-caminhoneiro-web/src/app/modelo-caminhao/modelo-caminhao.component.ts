import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { ModeloCaminhaoResultSemPaginacao } from './modeloCaminhaoResultSemPaginacao';
import { ModeloCaminhao } from './modeloCaminhao';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-modelo-caminhao',
  templateUrl: './modelo-caminhao.component.html',
  styleUrls: ['./modelo-caminhao.component.scss']
})
export class ModeloCaminhaoComponent implements OnInit {

  public modelosCaminhaoResultSemPaginacao: any;
  public displayedColumns: string[] = ['nome', 'ano', 'marca', 'Acao'];
  public modelosCaminhao!: MatTableDataSource<ModeloCaminhao>;
  public excluindoModelo!: boolean;
  public modeloCaminhaoResult: any;

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
      private http: HttpClient,
      @Inject('BASE_URL') private baseUrl: string,
      private toastr: ToastrService)
      { }

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this.http.get<ModeloCaminhaoResultSemPaginacao>(this.baseUrl + 'v1/modelo-caminhao/listar-todos-sem-paginacao')
    .subscribe(result => {
      this.modelosCaminhaoResultSemPaginacao = result;

      if (this.modelosCaminhaoResultSemPaginacao.isError) {
        this.toastr.error(this.modelosCaminhaoResultSemPaginacao.message, 'Modelos de Caminhão');
      } else {
        this.modelosCaminhao = new MatTableDataSource<ModeloCaminhao>(this.modelosCaminhaoResultSemPaginacao.result);
        this.modelosCaminhao.paginator = this.paginator;
      }
    }, error => console.error(error));
  }

  excluir(id: string) {
    this.excluindoModelo = true;

    this.http.delete(this.baseUrl + 'v1/modelo-caminhao/' + id)
      .subscribe(result => {
        this.excluindoModelo = false;
        this.modeloCaminhaoResult = result;
        this.toastr.success(this.modeloCaminhaoResult.result, 'Modelos de Caminhão');
        this.loadData();
      }, error => {
        this.excluindoModelo = false;
        this.toastr.error(error.error.title, 'Modelos de Caminhão');
      });
  }
}
