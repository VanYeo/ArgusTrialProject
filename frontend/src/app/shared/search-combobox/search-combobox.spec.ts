import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchCombobox } from './search-combobox';

describe('SearchCombobox', () => {
  let component: SearchCombobox;
  let fixture: ComponentFixture<SearchCombobox>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SearchCombobox]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SearchCombobox);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
