import { Empresa } from './../Models/empresa';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmpresaService {

  constructor(private http: HttpClient) { }

  enviarNotas(notas: Empresa) {

    this.http.post("https://localhost:44331/api/Empresa/SaveNotas", notas).subscribe(() => {
    }, error => console.log(error));  
  }

  carregarListaEmpresas(): Observable<any> {
    return this.http.get<any>(`https://localhost:44331/api/Empresa/SelectEmpresas`);
  }
}

