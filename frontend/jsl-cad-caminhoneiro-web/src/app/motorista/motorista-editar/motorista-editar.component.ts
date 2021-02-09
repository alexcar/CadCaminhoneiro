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
  public modelosCaminhaoFiltrado!: ModeloCaminhao[];

  public marcaSelecionadaId: string = '';
  public modeloSelecionadoId: string = '';

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
      enderecoDto: new FormGroup({
        logradouro: new FormControl('', Validators.required),
        numero: new FormControl('', Validators.required),
        complemento: new FormControl(''),
        bairro: new FormControl('', Validators.required),
        municipio: new FormControl('', Validators.required),
        uf: new FormControl('', Validators.required),
        cep: new FormControl('', Validators.required),
      }),
      habilitacaoDto: new FormGroup({
        numeroRegistro: new FormControl('', Validators.required),
        categoria: new FormControl('', Validators.required),
        dataPrimeiraHabilitacao: new FormControl('', Validators.required),
        dataValidade: new FormControl('', Validators.required),
        dataEmissao: new FormControl('', Validators.required),
        observacao: new FormControl(''),
      }),
      caminhaoDto: new FormGroup({
        marcaCaminhaoId: new FormControl('', Validators.required),
        modeloCaminhaoId: new FormControl('', Validators.required),
        placa: new FormControl('', Validators.required),
        eixo: new FormControl('', Validators.required),
        observacao: new FormControl(''),
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

  private loadModelosCaminhao() {
    this.http.get<ModeloCaminhaoResultSemPaginacao>(this.baseUrl + 'v1/modelo-caminhao/listar-todos-sem-paginacao')
    .subscribe(result => {
      this.modelosCaminhaoResult = result;

      if (this.modelosCaminhaoResult.isError) {
        this.toastr.error(this.modelosCaminhaoResult.message, 'Motorista');
      } else {
        this.modelosCaminhao = this.modelosCaminhaoResult.result;
        this.modelosCaminhaoFiltrado = this.modelosCaminhao;
      }
    }, error => {
      this.toastr.error(error.error.title, 'Motorista');
    });
  }

  onMarcaSelecionada(event: any) {
    const marcaCaminhaoId =  event.target.value;
    this.form.get('marcaCaminhaoId')?.setValue(marcaCaminhaoId);
    this.marcaSelecionadaId = marcaCaminhaoId;

    if (marcaCaminhaoId) {
      var modelosCaminhao: ModeloCaminhao[] = [];

      this.modelosCaminhao.forEach(modelo => {
        if(modelo.marcaCaminhaoListDto.id == marcaCaminhaoId) {
          modelosCaminhao.push(modelo);
        }
      });

      this.modelosCaminhaoFiltrado = modelosCaminhao;
    }
  }

  onModeloSelecionado(event: any) {
    const modeloCaminhaoId =  event.target.value;
    this.form.get('modeloCaminhaoId')?.setValue(modeloCaminhaoId);
    this.modeloSelecionadoId = modeloCaminhaoId;
  }

  onSubmit() {
    var motorista = (this.id) ? this.motorista : <Motorista>{};

    var foo = this.form;

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
    motorista.EnderecoDto.Id = this.form.get('enderecoDto.id')?.value;
    motorista.EnderecoDto.Logradouro = this.form.get('enderecoDto.logradouro')?.value;
    motorista.EnderecoDto.Numero = this.form.get('enderecoDto.numero')?.value;
    motorista.EnderecoDto.Complemento = this.form.get('enderecoDto.complemento')?.value;
    motorista.EnderecoDto.Bairro = this.form.get('enderecoDto.bairro')?.value;
    motorista.EnderecoDto.Municipio = this.form.get('enderecoDto.municipio')?.value;
    motorista.EnderecoDto.Uf = this.form.get('enderecoDto.uf')?.value;
    motorista.EnderecoDto.Cep = this.form.get('enderecoDto.cep')?.value;
    motorista.HabilitacaoDto.Id = this.form.get('habilitacaoDto.id')?.value;
    motorista.HabilitacaoDto.MotoristaId = this.form.get('habilitacaoDto.motoristaId')?.value;
    motorista.HabilitacaoDto.NumeroRegistro = this.form.get('habilitacaoDto.numeroRegistro')?.value;
    motorista.HabilitacaoDto.Categoria = this.form.get('habilitacaoDto.categoria')?.value;
    motorista.HabilitacaoDto.DataPrimeiraHabilitacao = this.form.get('habilitacaoDto.dataPrimeiraHabilitacao')?.value;
    motorista.HabilitacaoDto.DataValidade = this.form.get('habilitacaoDto.dataValidade')?.value;
    motorista.HabilitacaoDto.DataEmissao = this.form.get('habilitacaoDto.dataEmissao')?.value;
    motorista.HabilitacaoDto.Observacao = this.form.get('habilitacaoDto.observacao')?.value;
    motorista.CaminhaoDto.Id = this.form.get('caminhaoDto.id')?.value;
    motorista.CaminhaoDto.MotoristaId = this.form.get('caminhaoDto.motoristaId')?.value;
    motorista.CaminhaoDto.Placa = this.form.get('caminhaoDto.placa')?.value;
    motorista.CaminhaoDto.Eixo = this.form.get('caminhaoDto.eixo')?.value;
    motorista.CaminhaoDto.Observacao = this.form.get('caminhaoDto.observacao')?.value;

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
