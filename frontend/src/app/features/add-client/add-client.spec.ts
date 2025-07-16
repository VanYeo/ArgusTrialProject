import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddClient } from './add-client';

describe('AddClient', () => {
  let component: AddClient;
  let fixture: ComponentFixture<AddClient>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddClient]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddClient);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
