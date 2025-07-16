import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientsTable } from './clients-table';

describe('ClientsTable', () => {
  let component: ClientsTable;
  let fixture: ComponentFixture<ClientsTable>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ClientsTable]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClientsTable);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
