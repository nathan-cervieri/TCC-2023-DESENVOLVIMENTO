import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FileConverterComponent } from './file-converter.component';

describe('FileConverterComponent', () => {
  let component: FileConverterComponent;
  let fixture: ComponentFixture<FileConverterComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FileConverterComponent]
    });
    fixture = TestBed.createComponent(FileConverterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
