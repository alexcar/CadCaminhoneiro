import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { MarcaCaminhaoResult } from './marcaCaminhaoResult';
import { MarcasCaminhao } from './marcasCaminhao';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';

@Component({
  selector: 'app-marca-caminhao',
  templateUrl: './marca-caminhao.component.html',
  styleUrls: ['./marca-caminhao.component.css']
})
export class MarcaCaminhaoComponent implements OnInit {

  public marcasCaminhaoResult: any;
  public displayedColumns: string[] = ['nome'];

  // public marcasCaminhao: MarcasCaminhao[] = []

  // public marcasCaminhao: MatTableDataSource<MarcasCaminhao>;
  public marcasCaminhao!: MatTableDataSource<MarcasCaminhao>;

  // @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatPaginator) paginator!: MatPaginator;

    constructor(
      private http: HttpClient,
      @Inject('BASE_URL') private baseUrl: string)
      { }

  ngOnInit(): void {

    this.http.get<MarcaCaminhaoResult>(this.baseUrl + 'v1/marca-caminhao/listar-todos')
      .subscribe(result => {
        this.marcasCaminhaoResult = result;

        if (this.marcasCaminhaoResult.isError) {
          console.log(this.marcasCaminhaoResult.message);
        } else {
          // this.marcasCaminhao = this.marcasCaminhaoResult.result.data;
          this.marcasCaminhao = new MatTableDataSource<MarcasCaminhao>(this.marcasCaminhaoResult.result.data);
          this.marcasCaminhao.paginator = this.paginator;
        }
      }, error => console.error(error));
  }
}
