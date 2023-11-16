import { Component, EventEmitter, Output } from '@angular/core';

const FOLDER_UPLOAD_ID = 'folderUpload';

@Component({
  selector: 'app-folder-input',
  templateUrl: './folder-input.component.html',
  styleUrls: ['./folder-input.component.scss']
})
export class FolderInputComponent {

  @Output()
  uploadedFiles = new EventEmitter<File[]>();

  relevantFiles: File[] = [];

  folderUploaded(event: Event): void {
    const target = event.target as HTMLInputElement;
    const files = target.files as FileList;
    const relevantFiles = []

    for (let i = 0; i < files.length; i++) {
      const file = files[i];

      if (file.name.endsWith('.ts')) {
        relevantFiles.push(file);
      }
    }

    this.relevantFiles = relevantFiles;
  }

  emitFiles() {
    this.uploadedFiles.emit(this.relevantFiles);
  }

  clickFolderUpload(): void {
    document.getElementById(FOLDER_UPLOAD_ID)!.click();
  }

  clearFiles(): void {
    this.relevantFiles = [];
    this.uploadedFiles.emit([]);
  }

  shouldDisplayFileCount(): boolean {
    return this.relevantFiles?.length > 0;
  }

  shouldDisplayConvertButton(): boolean {
    return this.relevantFiles?.length > 0;
  }
}
