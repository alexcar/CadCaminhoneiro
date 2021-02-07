import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MotoristaEditarComponent } from './motorista-editar.component';

describe('MotoristaEditarComponent', () => {
  let component: MotoristaEditarComponent;
  let fixture: ComponentFixture<MotoristaEditarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MotoristaEditarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MotoristaEditarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
