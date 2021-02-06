import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MarcaCaminhaoComponent } from './marca-caminhao.component';

describe('MarcaCaminhaoComponent', () => {
  let component: MarcaCaminhaoComponent;
  let fixture: ComponentFixture<MarcaCaminhaoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MarcaCaminhaoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MarcaCaminhaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
