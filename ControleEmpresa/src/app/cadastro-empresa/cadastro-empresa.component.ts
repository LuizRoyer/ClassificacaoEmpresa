
import { Component, OnInit } from '@angular/core';
import * as XLSX from 'xlsx';

import { EmpresaService } from './empresa.service';
import { Empresa } from '../Models/empresa';
import { isUndefined } from 'util';
import { NotaItem } from '../Models/notaItem';

@Component({
  selector: 'app-cadastro-empresa',
  templateUrl: './cadastro-empresa.component.html',
  styleUrls: ['./cadastro-empresa.component.css']
})
export class CadastroEmpresaComponent implements OnInit {

  selectFiles: any;
  title: string = 'Entrada de Notas'
  constructor(private empresaService: EmpresaService) { }

  empresa: Empresa = new Empresa();
  listaEmpresas: NotaItem[];
  notas: [][];
  listaNotas: any[] = new Array();
  ngOnInit(): void {

    this.empresaService.carregarListaEmpresas().subscribe(result => {
      this.listaEmpresas = result;
    }, error => console.log(error));
  }

  //metodo para enviar a empresa e suas notas para a api , salvando no banco 
  enviar(empresa) {

    empresa.ListaNotas = this.listaNotas;
    if (!this.validarCampos(empresa)) return;

    this.empresaService.enviarNotas(empresa);

    // Força a pagina ser recarregada para atualizar o grafico
    new Promise((resolve, reject) => {
      setTimeout(() => {
        window.location.reload();
      }, 500)
    });
  }
  // metodo para capturar todas as notas selecionadas pelo usuario
  onChange(event) {

    this.selectFiles = event.target.files;

    if (this.selectFiles.length < 1) {
      console.log('vazio');
      this.listaNotas = [];
    }

    let fileNames = [];
    for (let i = 0; i < this.selectFiles.length; i++) {

      if (this.selectFiles[i].name.indexOf('xlsx') < 0) {
        alert('Informe um arquivo .xlsx do Excel ');
        document.getElementById('customFileLabel').innerHTML = "Selecionar Notas";
        return;
      }
      // gravar os nomes dos arquivos
      fileNames.push(this.selectFiles[i].name);

      // metodos para capturar o arquivo xlsx
      const reader: FileReader = new FileReader();
      reader.onload = (e: any) => {
        const bstr: string = e.target.result;

        const wb: XLSX.WorkBook = XLSX.read(bstr, { type: 'binary' });
        const wsname: string = wb.SheetNames[0];
        const ws: XLSX.WorkSheet = wb.Sheets[wsname];

        // atribui o Excel para a variavel notas
        this.notas = (XLSX.utils.sheet_to_json(ws, { header: 1 }));

        var item = new NotaItem();
        var inicio = 1;

        for (let i = 0; i < this.notas.length - 1; i++) {
          item.item[i] = parseInt(this.notas[inicio].toString().split(",")[0]);
          item.nome[i] = this.notas[inicio].toString().split(",")[1].toString();
          item.preco[i] = parseFloat(this.notas[inicio].toString().split(",")[2]);
          item.quantidade[i] = parseInt(this.notas[inicio].toString().split(",")[3]);
          item.data = this.notas[1].toString().split(",")[4];
          inicio++;
        }
        this.listaNotas.push(item);

      };
      reader.readAsBinaryString(this.selectFiles[i]);
    }
    //apresenta no campo os nomes dos arquivos selecionados
    document.getElementById('customFileLabel').innerHTML = fileNames.join(' , ');
  }

  // metodo para validar se possui campos nulos no Formulario
  validarCampos(empresa: Empresa): boolean {

    if (isUndefined(empresa.idEmpresa)) {
      alert("Campo Empresa é Obrigadotrio");
      return false;
    }
    if (isUndefined(empresa.tipoNota)) {
      alert("Campo Tipo Nota é Obrigadotrio");
      return false;
    }
    if (empresa.ListaNotas.length < 1) {
      alert("Selecione alguma Nota");
      return false;
    }
    return true;
  }
}
