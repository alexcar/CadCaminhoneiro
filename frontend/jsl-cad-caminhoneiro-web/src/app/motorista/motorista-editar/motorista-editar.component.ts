import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { formatDate } from '@angular/common';

import { Motorista } from './../motorista';
import { ModeloCaminhao } from './../../modelo-caminhao/modeloCaminhao';
import { MarcaCaminhao } from './../../marca-caminhao/marcaCaminhao';
import { MarcaCaminhaoResult } from './../../marca-caminhao/marcaCaminhaoResult';
import { ToastrService } from 'ngx-toastr';
import { MarcaCaminhaoResultSemPaginacao } from 'src/app/marca-caminhao/marcaCaminhaoResultSemPaginacao';
import { ModeloCaminhaoResultSemPaginacao } from 'src/app/modelo-caminhao/modeloCaminhaoResultSemPaginacao';
import { MotoristaIncluirRequest } from './../motoristaIncluirRequest';
import { MotoristaAlterarRequest } from '../motoristaAlterarRequest';
import { Validacoes } from './../validacoes';
import { Estado } from './../estados';

@Component({
  selector: 'app-motorista-editar',
  templateUrl: './motorista-editar.component.html',
  styleUrls: ['./motorista-editar.component.css']
})
export class MotoristaEditarComponent implements OnInit {

  public titulo!: string;
  public form!: FormGroup;
  public motorista!: Motorista;
  public motoristaIncluirRequest!: MotoristaIncluirRequest;
  public motoristaAlterarRequest!: MotoristaAlterarRequest;
  public motoristaResult: any;
  public modeloCaminhao!: ModeloCaminhao;
  public modeloCaminhaoResult: any;
  public id?: string;
  // public selectedMarca!: string;
  // public selectedModelo!: string;
  public motoristaResultEditar: any;
  public motoristaResultInserir: any;

  public marcasCaminhaoResult: any;
  public marcasCaminhao!: MarcaCaminhao[];

  public modelosCaminhaoResult: any;
  public modelosCaminhao!: ModeloCaminhao[];
  public modelosCaminhaoFiltrado!: ModeloCaminhao[];

  public marcaSelecionadaId: string = '';
  public modeloSelecionadoId: string = '';
  public estadoSelecionado: string = '';

  public dataNascimento!: any;
  public estados!: Estado[];
  public estadosResult: any;

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
      cpf: new FormControl('', Validators.compose([Validators.required, Validacoes.ValidaCpf])),
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

    // Carrega os estados
    this.loadEstados();

    // recupera o ID do parâmetro 'id'
    this.id = this.ActivatedRoute.snapshot.params.id;

    if (this.id) {
      // editar
      // Obtem o motorista do servidor
      var url = this.baseUrl + 'v1/motorista/' + this.id;

      this.http.get<Motorista>(url).subscribe(result => {
        this.motoristaResult = result;
        this.motorista = this.motoristaResult.result;
        this.titulo = "Edição - " + this.motorista.nome;

        this.marcaSelecionadaId = this.motorista.caminhaoDto.marcaCaminhaoListDto.id;
        this.modeloSelecionadoId = this.motorista.caminhaoDto.modeloCaminhaoListDto.id;
        this.estadoSelecionado = this.motorista.enderecoDto.uf;

        this.motorista.dataNascimento = formatDate(this.motorista.dataNascimento, 'yyyy-MM-dd', 'en');
        this.motorista.dataExpedicaoRegistroGeral = formatDate(this.motorista.dataExpedicaoRegistroGeral, 'yyyy-MM-dd', 'en');
        this.motorista.habilitacaoDto.dataPrimeiraHabilitacao = formatDate(this.motorista.habilitacaoDto.dataPrimeiraHabilitacao, 'yyyy-MM-dd', 'en');
        this.motorista.habilitacaoDto.dataValidade = formatDate(this.motorista.habilitacaoDto.dataValidade, 'yyyy-MM-dd', 'en');
        this.motorista.habilitacaoDto.dataEmissao = formatDate(this.motorista.habilitacaoDto.dataEmissao, 'yyyy-MM-dd', 'en');

        // atualiza o form com as informações do modelo
        this.form.patchValue(this.motorista);
        this.form.get('caminhaoDto.marcaCaminhaoId')?.setValue(this.motorista.caminhaoDto.marcaCaminhaoListDto.id);
        this.form.get('caminhaoDto.modeloCaminhaoId')?.setValue(this.motorista.caminhaoDto.modeloCaminhaoListDto.id);

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

  private loadEstados() {
    this.http.get<Estado>(this.baseUrl + 'v1/motorista/listar-estados')
      .subscribe(result => {
        this.estadosResult = result;

        if (this.estadosResult.isError) {
          this.toastr.error(this.estadosResult.message, 'Motorista');
        } else {
          this.estados = this.estadosResult.result;
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
    console.log(JSON.stringify(this.form.getRawValue()));

    if (this.id) {
      // editar
      this.motoristaAlterarRequest = <MotoristaAlterarRequest>{};
      this.motoristaAlterarRequest.id = this.id;
      this.motoristaAlterarRequest.nome = this.form.get('nome')?.value;
      this.motoristaAlterarRequest.cpf = this.form.get('cpf')?.value;
      this.motoristaAlterarRequest.dataNascimento = this.form.get('dataNascimento')?.value;
      this.motoristaAlterarRequest.nomePai = this.form.get('nomePai')?.value;
      this.motoristaAlterarRequest.nomeMae = this.form.get('nomeMae')?.value;
      this.motoristaAlterarRequest.naturalidade = this.form.get('naturalidade')?.value;
      this.motoristaAlterarRequest.numeroRegistroGeral = this.form.get('numeroRegistroGeral')?.value;
      this.motoristaAlterarRequest.orgaoExpedicaoRegistroGeral = this.form.get('orgaoExpedicaoRegistroGeral')?.value;
      this.motoristaAlterarRequest.dataExpedicaoRegistroGeral = this.form.get('dataExpedicaoRegistroGeral')?.value;
      this.motoristaAlterarRequest.enderecoId = this.motorista.enderecoDto.id;
      this.motoristaAlterarRequest.logradouro = this.form.get('enderecoDto.logradouro')?.value;
      this.motoristaAlterarRequest.numero = this.form.get('enderecoDto.numero')?.value;
      this.motoristaAlterarRequest.complemento = this.form.get('enderecoDto.complemento')?.value;
      this.motoristaAlterarRequest.bairro = this.form.get('enderecoDto.bairro')?.value;
      this.motoristaAlterarRequest.municipio = this.form.get('enderecoDto.municipio')?.value;
      this.motoristaAlterarRequest.uf = this.form.get('enderecoDto.uf')?.value;
      this.motoristaAlterarRequest.cep = this.form.get('enderecoDto.cep')?.value;
      this.motoristaAlterarRequest.habilitacaoId = this.motorista.habilitacaoDto.id;
      this.motoristaAlterarRequest.numeroRegistroHabilitacao = this.form.get('habilitacaoDto.numeroRegistro')?.value;
      this.motoristaAlterarRequest.categoriaHabilitacao = this.form.get('habilitacaoDto.categoria')?.value;
      this.motoristaAlterarRequest.dataPrimeiraHabilitacao = this.form.get('habilitacaoDto.dataPrimeiraHabilitacao')?.value;
      this.motoristaAlterarRequest.dataValidadeHabilitacao = this.form.get('habilitacaoDto.dataValidade')?.value;
      this.motoristaAlterarRequest.dataEmissaoHabilitacao = this.form.get('habilitacaoDto.dataEmissao')?.value;
      this.motoristaAlterarRequest.observacaoHabilitacao = this.form.get('habilitacaoDto.observacao')?.value;
      this.motoristaAlterarRequest.caminhaoId = this.motorista.caminhaoDto.id;
      this.motoristaAlterarRequest.placa = this.form.get('caminhaoDto.placa')?.value;
      this.motoristaAlterarRequest.eixo = this.form.get('caminhaoDto.eixo')?.value;
      this.motoristaAlterarRequest.marcaCaminhaoId = this.form.get('caminhaoDto.marcaCaminhaoId')?.value;
      this.motoristaAlterarRequest.modeloCaminhaoId = this.form.get('caminhaoDto.modeloCaminhaoId')?.value;
      this.motoristaAlterarRequest.caminhaoObservacao = this.form.get('caminhaoDto.observacao')?.value;

      var url = this.baseUrl + 'v1/motorista/' + this.motorista.id;
      this.http
        .put<Motorista>(url, this.motoristaAlterarRequest)
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
      this.motoristaIncluirRequest = <MotoristaIncluirRequest>{};
      this.motoristaIncluirRequest.nome = this.form.get('nome')?.value;
      this.motoristaIncluirRequest.cpf = this.form.get('cpf')?.value;
      this.motoristaIncluirRequest.dataNascimento = this.form.get('dataNascimento')?.value;
      this.motoristaIncluirRequest.nomePai = this.form.get('nomePai')?.value;
      this.motoristaIncluirRequest.nomeMae = this.form.get('nomeMae')?.value;
      this.motoristaIncluirRequest.naturalidade = this.form.get('naturalidade')?.value;
      this.motoristaIncluirRequest.numeroRegistroGeral = this.form.get('numeroRegistroGeral')?.value;
      this.motoristaIncluirRequest.orgaoExpedicaoRegistroGeral = this.form.get('orgaoExpedicaoRegistroGeral')?.value;
      this.motoristaIncluirRequest.dataExpedicaoRegistroGeral = this.form.get('dataExpedicaoRegistroGeral')?.value;
      this.motoristaIncluirRequest.logradouro = this.form.get('enderecoDto.logradouro')?.value;
      this.motoristaIncluirRequest.numero = this.form.get('enderecoDto.numero')?.value;
      this.motoristaIncluirRequest.complemento = this.form.get('enderecoDto.complemento')?.value;
      this.motoristaIncluirRequest.bairro = this.form.get('enderecoDto.bairro')?.value;
      this.motoristaIncluirRequest.municipio = this.form.get('enderecoDto.municipio')?.value;
      this.motoristaIncluirRequest.uf = this.form.get('enderecoDto.uf')?.value;
      this.motoristaIncluirRequest.cep = this.form.get('enderecoDto.cep')?.value;
      this.motoristaIncluirRequest.numeroRegistroHabilitacao = this.form.get('habilitacaoDto.numeroRegistro')?.value;
      this.motoristaIncluirRequest.categoriaHabilitacao = this.form.get('habilitacaoDto.categoria')?.value;
      this.motoristaIncluirRequest.dataPrimeiraHabilitacao = this.form.get('habilitacaoDto.dataPrimeiraHabilitacao')?.value;
      this.motoristaIncluirRequest.dataValidadeHabilitacao = this.form.get('habilitacaoDto.dataValidade')?.value;
      this.motoristaIncluirRequest.dataEmissaoHabilitacao = this.form.get('habilitacaoDto.dataEmissao')?.value;
      this.motoristaIncluirRequest.observacaoHabilitacao = this.form.get('habilitacaoDto.observacao')?.value;
      this.motoristaIncluirRequest.placa = this.form.get('caminhaoDto.placa')?.value;
      this.motoristaIncluirRequest.eixo = this.form.get('caminhaoDto.eixo')?.value;
      this.motoristaIncluirRequest.marcaCaminhaoId = this.form.get('caminhaoDto.marcaCaminhaoId')?.value;
      this.motoristaIncluirRequest.modeloCaminhaoId = this.form.get('caminhaoDto.modeloCaminhaoId')?.value;
      this.motoristaIncluirRequest.caminhaoObservacao = this.form.get('caminhaoDto.observacao')?.value;

      var url = this.baseUrl + 'v1/motorista/';
      this.http
        .post<Motorista>(url, this.motoristaIncluirRequest)
        .subscribe(result => {
          this.motoristaResultInserir = result;
          this.toastr.success(this.motoristaResultInserir.result, 'Motorista');

          // voltar para a listagem
          this.router.navigate(['/motoristas']);
        }, error => {
          if (error.error.validationErrors){
            var mensagensErro = 'Não foi possível incluir o motorista:<br/><ul>';

            error.error.validationErrors.forEach((erro: { reason: string; }) => {
              mensagensErro = mensagensErro + '<li>' + erro.reason + '</li>';
            });

            mensagensErro = mensagensErro + '</ul>';

            this.toastr.error(mensagensErro, 'Motorista', {
              enableHtml: true, timeOut: 10000 });
          } else {
            this.toastr.error(error.error.title, 'Motorista');
          }
        });
    }
  }
}
