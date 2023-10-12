import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SingleCodeConverterComponent } from './single-code-converter.component';

describe('SingleCodeConverterComponent', () => {
  let component: SingleCodeConverterComponent;
  let fixture: ComponentFixture<SingleCodeConverterComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SingleCodeConverterComponent]
    });
    fixture = TestBed.createComponent(SingleCodeConverterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
