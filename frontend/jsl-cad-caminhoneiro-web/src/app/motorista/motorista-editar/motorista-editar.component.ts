import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { Motorista } from './../motorista';
import { ModeloCaminhao } from './../../modelo-caminhao/modeloCaminhao';
import { MarcaCaminhao } from './../../marca-caminhao/marcaCaminhao';
import { MarcaCaminhaoResult } from './../../marca-caminhao/marcaCaminhaoResult';
import { ToastrService } from 'ngx-toastr';
import { MarcaCaminhaoResultSemPaginacao } from 'src/app/marca-caminhao/marcaCaminhaoResultSemPaginacao';
import { ModeloCaminhaoResultSemPaginacao } from 'src/app/modelo-caminhao/modeloCaminhaoResultSemPaginacao';

@Component({
  selector: 'app-motorista-editar',
  templateUrl: './motorista-editar.component.html',
  styleUrls: ['./motorista-editar.component.css']
})
export class MotoristaEditarComponent implements OnInit {

  public titulo!: string;
  public form!: FormGroup;
  public motorista!: Motorista;
  public motoristaResult: any;
  public modeloCaminhao!: ModeloCaminhao;
  public modeloCaminhaoResult: any;
  public id?: string;
  public selectedMarca!: string;
  public selectedModelo!: string;
  public motoristaResultEditar: any;
  public motoristaResultInserir: any;

  public marcasCaminhaoResult: any;
  public marcasCaminhao!: MarcaCaminhao[];

  public modelosCaminhaoResult: any;
  public modelosCaminhao!: ModeloCaminhao[];

  constructor(
    private ActivatedRoute: ActivatedRoute,
    private router: Router,
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.form = new FormGroup({
      nome: new FormControl('', Validators.required),
      cpf: new FormControl('', Validators.required),
      dataNascimento: new FormControl('', Validators.required),
      nomePai: new FormControl('', Validators.required),
      nomeMae: new FormControl('', Validators.required),
      naturalidade: new FormControl('', Validators.required),
      numeroRegistroGeral: new FormControl('', Validators.required),
      orgaoExpedicaoRegistroGeral: new FormControl('', Validators.required),
      dataExpedicaoRegistroGeral: new FormControl('', Validators.required),
      EnderecoDto: new FormGroup({
        Logradouro: new FormControl('', Validators.required),
        Numero: new FormControl('', Validators.required),
        Complemento: new FormControl('', Validators.required),
        Bairro: new FormControl('', Validators.required),
        Municipio: new FormControl('', Validators.required),
        Uf: new FormControl('', Validators.required),
        Cep: new FormControl('', Validators.required),
      }),
      HabilitacaoDto: new FormGroup({
        NumeroRegistro: new FormControl('', Validators.required),
        Categoria: new FormControl('', Validators.required),
        DataPrimeiraHabilitacao: new FormControl('', Validators.required),
        DataValidade: new FormControl('', Validators.required),
        DataEmissao: new FormControl('', Validators.required),
        Observacao: new FormControl('', Validators.required),
      }),
      CaminhaoDto: new FormGroup({
        Placa: new FormControl('', Validators.required),
        Eixo: new FormControl('', Validators.required),
        Observacao: new FormControl('', Validators.required),
      })
    });

    this.loadData();
  }

  get f() { return this.form.controls; }

  loadData() {
    // Carrega as marcas
    this.loadMarcasCaminhao();

    // Carrega os modelos
    this.loadModelosCaminhao();

    // recupera o ID do parâmetro 'id'
    this.id = this.ActivatedRoute.snapshot.params.id;

    if (this.id) {
      // editar
      // Obtem o motorista do servidor
      var url = this.baseUrl + 'v1/motorista/' + this.id;

      this.http.get<Motorista>(url).subscribe(result => {
        this.motoristaResult = result;
        this.motorista = this.modeloCaminhaoResult.result;
        // this.modeloCaminhao.marcaCaminhaoId = this.modeloCaminhao.marcaCaminhaoListDto.id;
        this.titulo = "Edição - " + this.motorista.nome;
        this.selectedMarca = this.motorista.CaminhaoDto.MarcaCaminhaoListDto.id;
        this.selectedModelo = this.motorista.CaminhaoDto.ModeloCaminhaoListDto.id;

        // atualiza o form com as informações do modelo
        this.form.patchValue(this.motorista);
      }, error => {
        this.toastr.error(error.error.title, 'Motorista');
      });

    } else {
      // inserir
      this.titulo = "Inclusão"
    }
  }

  private loadMarcasCaminhao() {
    this.http.get<MarcaCaminhaoResultSemPaginacao>(this.baseUrl + 'v1/marca-caminhao/listar-todos-sem-paginacao')
      .subscribe(result => {
        this.marcasCaminhaoResult = result;

        if (this.marcasCaminhaoResult.isError) {
          this.toastr.error(this.marcasCaminhaoResult.message, 'Motorista');
        } else {
          this.marcasCaminhao = this.marcasCaminhaoResult.result;
        }
      }, error => {
        this.toastr.error(error.error.title, 'Motorista');
      });
  }

  // TODO: Passar o id da marca para filtrar os modelos
  private loadModelosCaminhao() {
    this.http.get<ModeloCaminhaoResultSemPaginacao>(this.baseUrl + 'v1/marca-caminhao/listar-todos-sem-paginacao')
    .subscribe(result => {
      this.modelosCaminhaoResult = result;

      if (this.modelosCaminhaoResult.isError) {
        this.toastr.error(this.modelosCaminhaoResult.message, 'Motorista');
      } else {
        this.modelosCaminhao = this.modelosCaminhaoResult.result;
      }
    }, error => {
      this.toastr.error(error.error.title, 'Motorista');
    });
  }

  onSubmit() {
    var motorista = (this.id) ? this.motorista : <Motorista>{};

    motorista.id = this.form.get('id')?.value;
    motorista.nome = this.form.get('nome')?.value;
    motorista.cpf = this.form.get('cpf')?.value;
    motorista.dataNascimento = this.form.get('dataNascimento')?.value;
    motorista.nomePai = this.form.get('nomePai')?.value;
    motorista.nomeMae = this.form.get('nomeMae')?.value;
    motorista.naturalidade = this.form.get('naturalidade')?.value;
    motorista.numeroRegistroGeral = this.form.get('numeroRegistroGeral')?.value;
    motorista.orgaoExpedicaoRegistroGeral = this.form.get('orgaoExpedicaoRegistroGeral')?.value;
    motorista.dataExpedicaoRegistroGeral = this.form.get('dataExpedicaoRegistroGeral')?.value;
    motorista.EnderecoDto = this.form.get('EnderecoDto.Logradouro')?.value;
    // motorista. = this.form.get('')?.value;
    // motorista. = this.form.get('')?.value;
    // motorista. = this.form.get('')?.value;
    // motorista. = this.form.get('')?.value;
    // motorista. = this.form.get('')?.value;
    // motorista. = this.form.get('')?.value;
    // motorista. = this.form.get('')?.value;
    // motorista. = this.form.get('')?.value;
    // motorista. = this.form.get('')?.value;

    if (this.id) {
      // editar

      var url = this.baseUrl + 'v1/motorista/' + this.motorista.id;
      this.http
        .put<Motorista>(url, motorista)
          .subscribe(result => {
            this.motoristaResultEditar = result;
            this.toastr.success(this.motoristaResultEditar.result, 'Motorista');

            // volta para a listagem
            this.router.navigate(['/motoristas']);
          }, error => {
            this.toastr.error(error.error.title, 'Motorista');
          });
    } else {
      // inserir

      var url = this.baseUrl + 'v1/motorista/';
      this.http
        .post<Motorista>(url, motorista)
        .subscribe(result => {
          this.motoristaResultInserir = result;
          this.toastr.success(this.motoristaResultInserir.result, 'Motorista');

          // voltar para a listagem
          this.router.navigate(['/motoristas']);
        }, error => {
          this.toastr.error(error.error.title, 'Motorista');
        });
    }
  }
}
