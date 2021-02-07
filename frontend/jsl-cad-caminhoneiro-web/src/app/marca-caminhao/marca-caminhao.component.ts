import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

import { MarcaCaminhaoResultSemPaginacao } from './marcaCaminhaoResultSemPaginacao';
import { MarcaCaminhao } from './marcaCaminhao';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-marca-caminhao',
  templateUrl: './marca-caminhao.component.html',
  styleUrls: ['./marca-caminhao.component.css']
})
export class MarcaCaminhaoComponent implements OnInit {

  public marcasCaminhaoResultSemPaginacao: any;
  public marcaCaminhaoResult: any;
  public displayedColumns: string[] = ['nome', 'Acao'];
  public excluindoMarca!: boolean;
  public marcasCaminhao!: MatTableDataSource<MarcaCaminhao>;

  // @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatPaginator) paginator!: MatPaginator;

    constructor(
      private http: HttpClient,
      private router: Router,
      @Inject('BASE_URL') private baseUrl: string,
      private toastr: ToastrService)
      { }

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this.http.get<MarcaCaminhaoResultSemPaginacao>(this.baseUrl + 'v1/marca-caminhao/listar-todos-sem-paginacao')
    .subscribe(result => {
      this.marcasCaminhaoResultSemPaginacao = result;

      if (this.marcasCaminhaoResultSemPaginacao.isError) {
        this.toastr.error(this.marcasCaminhaoResultSemPaginacao.message, 'Marcas de Caminhão');
      } else {
            this.marcasCaminhao = new MatTableDataSource<MarcaCaminhao>(this.marcasCaminhaoResultSemPaginacao.result);
        this.marcasCaminhao.paginator = this.paginator;
      }
    }, error => console.error(error));
  }

  excluir(id: string) {
    this.excluindoMarca = true;

    this.http.delete(this.baseUrl + 'v1/marca-caminhao/' + id)
      .subscribe(result => {
        this.excluindoMarca = false;
        this.marcaCaminhaoResult = result;
        this.toastr.success(this.marcaCaminhaoResult.result, 'Marcas de Caminhão');
        this.loadData();
      }, error => {
        this.excluindoMarca = false;
        this.toastr.error(error.error.title, 'Marcas de Caminhão');
      });
  }
}
