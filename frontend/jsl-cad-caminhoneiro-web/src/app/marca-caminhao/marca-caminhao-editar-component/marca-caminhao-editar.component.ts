import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { MarcaCaminhao } from '../marcaCaminhao';

@Component({
  selector: 'app-marca-caminhao-editar-component',
  templateUrl: './marca-caminhao-editar.component.html',
  styleUrls: ['./marca-caminhao-editar.component.scss']
})
export class MarcaCaminhaoEditarComponent implements OnInit {

  public titulo!: string;
  public form!: FormGroup;
  public marcaCaminhao!: MarcaCaminhao;
  public marcaCaminhaoResult: any;
  public id?: string;
  public marcaCaminhaoResultEditar: any;
  public marcaCaminhaoResultInserir: any;

  constructor(
    private ActivatedRoute: ActivatedRoute,
    private router: Router,
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private toastr: ToastrService) { }

  ngOnInit() {
    this.form = new FormGroup({
      descricao: new FormControl('', Validators.required)
    });

    this.loadData();
  }

  get descricao() { return this.form.get('descricao'); }

  loadData() {
    // recupera o ID do parâmetro 'id'
    this.id = this.ActivatedRoute.snapshot.params.id;
    // var id = + !this.ActivatedRoute.snapshot.paramMap.get('id');
    // console.log(id);

    if (this.id) {
      // editar
      // Obtem a marca do servidor
      var url = this.baseUrl + 'v1/marca-caminhao/' + this.id;


      this.http.get<MarcaCaminhao>(url).subscribe(result => {
        this.marcaCaminhaoResult = result;
        this.marcaCaminhao = this.marcaCaminhaoResult.result;
        this.titulo = "Edição - " + this.marcaCaminhao.descricao;

        // atualiza o form com as informações da marca
        this.form.patchValue(this.marcaCaminhao);
      }, error => console.error(error));

    } else {
      // inserir
      this.titulo = "Inclusão"
    }
  }

  onSubmit() {
    // var marcaCaminhao = this.marcaCaminhao;
    var marcaCaminhao = (this.id) ? this.marcaCaminhao : <MarcaCaminhao>{};

    marcaCaminhao.descricao = this.form.get('descricao')?.value;

    if (this.id) {
      // editar

      var url = this.baseUrl + 'v1/marca-caminhao/' + this.marcaCaminhao.id;
      this.http
        .put<MarcaCaminhao>(url, marcaCaminhao)
          .subscribe(result => {
            this.marcaCaminhaoResultEditar = result;
            this.toastr.success(this.marcaCaminhaoResultEditar.result, 'Marcas de Caminhão');

            // volta para a listagem
            this.router.navigate(['/marcasCaminhao']);
          }, error => {
            this.toastr.error(error.error.title, 'Marcas de Caminhão');
          });
    } else {
      // inserir

      var url = this.baseUrl + 'v1/marca-caminhao/';
      this.http
        .post<MarcaCaminhao>(url, marcaCaminhao)
        .subscribe(result => {
          this.marcaCaminhaoResultInserir = result;
          this.toastr.success(this.marcaCaminhaoResultInserir.result, 'Marcas de Caminhão');

          // voltar para a listagem
          this.router.navigate(['/marcasCaminhao']);
        }, error => {
          this.toastr.error(error.error.title, 'Marcas de Caminhão');
        });
    }
  }
}
