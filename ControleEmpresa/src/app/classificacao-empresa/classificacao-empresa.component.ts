import { Component, OnInit } from '@angular/core';
import { ChartOptions, ChartType, ChartDataSets } from 'chart.js';
import { Label } from 'ng2-charts';

import { EmpresaView } from '../Models/empresaView';
import { EmpresaService } from './../cadastro-empresa/empresa.service';

@Component({
  selector: 'app-classificacao-empresa',
  templateUrl: './classificacao-empresa.component.html',
  styleUrls: ['./classificacao-empresa.component.css']
})
export class ClassificacaoEmpresaComponent implements OnInit {

  empresas: EmpresaView[];
  anychart: any;
  constructor(private empresaService: EmpresaService) { }

  ngOnInit(): void {

    this.empresaService.carregarListaEmpresas().subscribe(result => {
      this.empresas = result;

      // montar o grafico com as classificaçoes da empresa
      this.barChartData = [
        { data: [this.empresas[0].classificacao], label: this.empresas[0].nome },
        { data: [this.empresas[1].classificacao], label: this.empresas[1].nome },
        { data: [this.empresas[2].classificacao], label: this.empresas[2].nome },
        { data: [this.empresas[3].classificacao], label: this.empresas[3].nome },
        { data: [this.empresas[4].classificacao], label: this.empresas[4].nome },
        { data: [this.empresas[5].classificacao], label: this.empresas[5].nome },
        { data: [this.empresas[6].classificacao], label: this.empresas[6].nome }
      ];
    }, error => console.log(error));
  }

  //Componete para montar o Grafico da Empresa e sua Classificação
  barChartOptions: ChartOptions = {
    responsive: true,
    scales: { xAxes: [{}], yAxes: [{}] },
  };
  barChartLabels: Label[] = ['Classificação %'];
  barChartType: ChartType = 'bar';
  barChartLegend = true;
  barChartPlugins = [];

  barChartData: ChartDataSets[] = [
    { data: [50], label: 'Teste' }
  ];
  
}
