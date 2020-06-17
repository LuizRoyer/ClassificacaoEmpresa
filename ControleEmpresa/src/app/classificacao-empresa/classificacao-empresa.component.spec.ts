import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ClassificacaoEmpresaComponent } from './classificacao-empresa.component';

describe('ClassificacaoEmpresaComponent', () => {
  let component: ClassificacaoEmpresaComponent;
  let fixture: ComponentFixture<ClassificacaoEmpresaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ClassificacaoEmpresaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ClassificacaoEmpresaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
