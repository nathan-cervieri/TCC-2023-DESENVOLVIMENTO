import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FileConverterListComponent } from './file-converter-list.component';

describe('FileConverterListComponent', () => {
  let component: FileConverterListComponent;
  let fixture: ComponentFixture<FileConverterListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FileConverterListComponent]
    });
    fixture = TestBed.createComponent(FileConverterListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
