import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { ModeloCaminhao } from '../modeloCaminhao';
import { MarcaCaminhao } from './../../marca-caminhao/marcaCaminhao';
import { MarcaCaminhaoResult } from './../../marca-caminhao/marcaCaminhaoResult';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-modelo-caminhao-editar',
  templateUrl: './modelo-caminhao-editar.component.html',
  styleUrls: ['./modelo-caminhao-editar.component.scss']
})
export class ModeloCaminhaoEditarComponent implements OnInit {

  public titulo!: string;
  public form!: FormGroup;
  public modeloCaminhao!: ModeloCaminhao;
  public modeloCaminhaoResult: any;
  public id?: string;
  public selected!: string;
  public modeloCaminhaoResultEditar: any;
  public modeloCaminhaoResultInserir: any;

  public marcasCaminhaoResult: any;
  public marcasCaminhao!: MarcaCaminhao[];

  constructor(private ActivatedRoute: ActivatedRoute,
    private router: Router,
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private toastr: ToastrService) { }

  ngOnInit() {
    this.form = new FormGroup({
      descricao: new FormControl('', Validators.required),
      ano: new FormControl('', Validators.required),
      marcaCaminhaoId: new FormControl('', Validators.required)
    });

    this.loadData();
  }

  get descricao() { return this.form.get('descricao'); }
  get ano() { return this.form.get('ano'); }
  get marcaCaminhaoId() { return this.form.get('modeloCaminhao.marcaCaminhaoListDto.id'); }

  loadData() {
    // Carrega as marcas
    this.loadMarcasCaminhao();

    // recupera o ID do parâmetro 'id'
    this.id = this.ActivatedRoute.snapshot.params.id;

    if (this.id) {
      // editar
      // Obtem o modelo do servidor
      var url = this.baseUrl + 'v1/modelo-caminhao/' + this.id;

      this.http.get<ModeloCaminhao>(url).subscribe(result => {
        this.modeloCaminhaoResult = result;
        this.modeloCaminhao = this.modeloCaminhaoResult.result;
        this.modeloCaminhao.marcaCaminhaoId = this.modeloCaminhao.marcaCaminhaoListDto.id;
        this.titulo = "Edição - " + this.modeloCaminhao.descricao;
        this.selected = this.modeloCaminhao.marcaCaminhaoListDto.id;

        // atualiza o form com as informações do modelo
        this.form.patchValue(this.modeloCaminhao);
      }, error => {
        this.toastr.error(error.error.title, 'Modelos de Caminhão');
      });

    } else {
      // inserir
      this.titulo = "Inclusão"
    }
  }

  loadMarcasCaminhao() {
    this.http.get<MarcaCaminhaoResult>(this.baseUrl + 'v1/marca-caminhao/listar-todos')
      .subscribe(result => {
        this.marcasCaminhaoResult = result;

        if (this.marcasCaminhaoResult.isError) {
          console.log(this.marcasCaminhaoResult.message);
        } else {
          this.marcasCaminhao = this.marcasCaminhaoResult.result.data;
        }
      }, error => console.error(error));
  }

  onSubmit() {
    var modeloCaminhao = (this.id) ? this.modeloCaminhao : <ModeloCaminhao>{};

    modeloCaminhao.descricao = this.form.get('descricao')?.value;
    modeloCaminhao.ano = this.form.get('ano')?.value;
    modeloCaminhao.marcaCaminhaoId = this.form.get('marcaCaminhaoId')?.value;

    if (this.id) {
      // editar

      var url = this.baseUrl + 'v1/modelo-caminhao/' + this.modeloCaminhao.id;
      this.http
        .put<ModeloCaminhao>(url, modeloCaminhao)
          .subscribe(result => {
            this.modeloCaminhaoResultEditar = result;
            this.toastr.success(this.modeloCaminhaoResultEditar.result, 'Modelos de Caminhão');

            // volta para a listagem
            this.router.navigate(['/modelosCaminhao']);
          }, error => {
            this.toastr.error(error.error.title, 'Modelos de Caminhão');
          });
    } else {
      // inserir

      var url = this.baseUrl + 'v1/modelo-caminhao/';
      this.http
        .post<ModeloCaminhao>(url, modeloCaminhao)
        .subscribe(result => {
          this.modeloCaminhaoResultInserir = result;
          this.toastr.success(this.modeloCaminhaoResultInserir.result, 'Modelos de Caminhão');

          // voltar para a listagem
          this.router.navigate(['/modelosCaminhao']);
        }, error => {
          this.toastr.error(error.error.title, 'Modelos de Caminhão');
        });
    }
  }
}
