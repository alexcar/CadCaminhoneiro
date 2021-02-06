import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

import { MarcaCaminhaoResult } from './marcaCaminhaoResult';
import { MarcaCaminhao } from './marcaCaminhao';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';

@Component({
  selector: 'app-marca-caminhao',
  templateUrl: './marca-caminhao.component.html',
  styleUrls: ['./marca-caminhao.component.css']
})
export class MarcaCaminhaoComponent implements OnInit {

  public marcasCaminhaoResult: any;
  public displayedColumns: string[] = ['nome', 'Acao'];
  excluindoMarca!: boolean;

  // public marcasCaminhao: MarcasCaminhao[] = []

  // public marcasCaminhao: MatTableDataSource<MarcasCaminhao>;
  public marcasCaminhao!: MatTableDataSource<MarcaCaminhao>;

  // @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatPaginator) paginator!: MatPaginator;

    constructor(
      private http: HttpClient,
      private router: Router,
      @Inject('BASE_URL') private baseUrl: string)
      { }

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this.http.get<MarcaCaminhaoResult>(this.baseUrl + 'v1/marca-caminhao/listar-todos')
    .subscribe(result => {
      this.marcasCaminhaoResult = result;

      if (this.marcasCaminhaoResult.isError) {
        console.log(this.marcasCaminhaoResult.message);
      } else {
            this.marcasCaminhao = new MatTableDataSource<MarcaCaminhao>(this.marcasCaminhaoResult.result.data);
        this.marcasCaminhao.paginator = this.paginator;
      }
    }, error => console.error(error));
  }

  excluir(id: string) {
    this.excluindoMarca = true;

    this.http.delete(this.baseUrl + 'v1/marca-caminhao/' + id)
      .subscribe(result => {
        this.excluindoMarca = false;
        this.loadData();
      }, error => {
        this.excluindoMarca = false;
        console.error(error)

      });
  }
}
