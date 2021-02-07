import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { MotoristaResultSemPaginacao } from './motoristaResultSemPaginacao';
import { Motorista } from './motorista';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-motorista',
  templateUrl: './motorista.component.html',
  styleUrls: ['./motorista.component.css']
})
export class MotoristaComponent implements OnInit {

  public motoristasResultSemPaginacao: any;
  public displayedColumns: string[] = ['nome', 'cpf', 'rg', 'Acao'];
  public motoristas!: MatTableDataSource<Motorista>;
  public excluindoMotorista!: boolean;
  public motoristaResult: any;

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this.http.get<MotoristaResultSemPaginacao>(this.baseUrl + 'v1/motorista/listar-todos-sem-paginacao')
    .subscribe(result => {
      this.motoristasResultSemPaginacao = result;

      if (this.motoristasResultSemPaginacao.isError) {
        this.toastr.error(this.motoristasResultSemPaginacao.message, 'Motoristas');
      } else {
        this.motoristas = new MatTableDataSource<Motorista>(this.motoristasResultSemPaginacao.result);
        this.motoristas.paginator = this.paginator;
      }
    }, error => console.error(error));
  }

  excluir(id: string) {
    this.excluindoMotorista = true;

    this.http.delete(this.baseUrl + 'v1/motorista/' + id)
      .subscribe(result => {
        this.excluindoMotorista = false;
        this.motoristaResult = result;
        this.toastr.success(this.motoristaResult.result, 'Motoristas');
        this.loadData();
      }, error => {
        this.excluindoMotorista = false;
        this.toastr.error(error.error.title, 'Motoristas');
      });
  }
}
