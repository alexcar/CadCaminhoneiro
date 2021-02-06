import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { ModeloCaminhaoResult } from './modeloCaminhaoResult';
import { ModeloCaminhao } from './modeloCaminhao';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';

@Component({
  selector: 'app-modelo-caminhao',
  templateUrl: './modelo-caminhao.component.html',
  styleUrls: ['./modelo-caminhao.component.scss']
})
export class ModeloCaminhaoComponent implements OnInit {

  public modelosCaminhaoResult: any;
  public displayedColumns: string[] = ['nome', 'ano', 'marca'];
  public modelosCaminhao!: MatTableDataSource<ModeloCaminhao>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
      private http: HttpClient,
      @Inject('BASE_URL') private baseUrl: string)
      { }

  ngOnInit() {
    this.http.get<ModeloCaminhaoResult>(this.baseUrl + 'v1/modelo-caminhao/listar-todos')
      .subscribe(result => {
        this.modelosCaminhaoResult = result;

        if (this.modelosCaminhaoResult.isError) {
          console.log(this.modelosCaminhaoResult.message);
        } else {
          this.modelosCaminhao = new MatTableDataSource<ModeloCaminhao>(this.modelosCaminhaoResult.result.data);
          this.modelosCaminhao.paginator = this.paginator;
        }
      }, error => console.error(error));
  }
}
