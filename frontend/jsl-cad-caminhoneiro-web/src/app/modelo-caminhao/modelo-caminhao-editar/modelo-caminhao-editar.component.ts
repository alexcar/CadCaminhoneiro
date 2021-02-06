import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl } from '@angular/forms';

import { ModeloCaminhao } from '../modeloCaminhao';
import { MarcaCaminhao } from './../../marca-caminhao/marcaCaminhao';
import { MarcaCaminhaoResult } from './../../marca-caminhao/marcaCaminhaoResult';

@Component({
  selector: 'app-modelo-caminhao-editar',
  templateUrl: './modelo-caminhao-editar.component.html',
  styleUrls: ['./modelo-caminhao-editar.component.scss']
})
export class ModeloCaminhaoEditarComponent implements OnInit {

  titulo!: string;
  form!: FormGroup;
  modeloCaminhao!: ModeloCaminhao;
  modeloCaminhaoResult: any;
  id?: string;
  selected!: string;

  marcasCaminhaoResult: any;
  marcasCaminhao!: MarcaCaminhao[];

  constructor(private ActivatedRoute: ActivatedRoute,
    private router: Router,
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string) { }

  ngOnInit() {
    this.form = new FormGroup({
      descricao: new FormControl(''),
      ano: new FormControl(''),
      marcaCaminhaoId: new FormControl('')
    });

    this.loadData();
  }

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
        this.titulo = "Edição - " + this.modeloCaminhao.descricao;
        this.selected = this.modeloCaminhao.marcaCaminhaoListDto.id;

        // atualiza o form com as informações do modelo
        this.form.patchValue(this.modeloCaminhao);
      }, error => console.error(error));

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
            console.log("Modelo Caminhão" + modeloCaminhao.id + " atualizado com sucesso");

            // volta para a listagem
            this.router.navigate(['/modelosCaminhao']);
          }, error => console.error(error));
    } else {
      // inserir

      var url = this.baseUrl + 'v1/modelo-caminhao/';
      this.http
        .post<ModeloCaminhao>(url, modeloCaminhao)
        .subscribe(result => {
          console.log("Modelo " + result.id + "cadastrada com sucesso");

          // voltar para a listagem
          this.router.navigate(['/modelosCaminhao']);
        }, error => console.log(error));
    }
  }
}
