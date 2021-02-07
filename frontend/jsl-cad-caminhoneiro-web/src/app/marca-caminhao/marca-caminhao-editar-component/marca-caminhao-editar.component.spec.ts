/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { MarcaCaminhaoEditarComponent } from './marca-caminhao-editar.component';


describe('MarcaCaminhaoEditarComponentComponent', () => {
  let component: MarcaCaminhaoEditarComponent;
  let fixture: ComponentFixture<MarcaCaminhaoEditarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MarcaCaminhaoEditarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MarcaCaminhaoEditarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
