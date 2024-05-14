/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { supportComponent } from './support.component';

describe('SupportComponent', () => {
  let component: supportComponent;
  let fixture: ComponentFixture<supportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ supportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(supportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
