import { Component, inject } from '@angular/core';
import { FileService } from '../shared/file.service';

@Component({
  selector: 'app-file-upload',
  imports: [],
  templateUrl: './file-upload.html',
  styleUrl: './file-upload.css',
})
export class FileUpload {
  private fileService = inject(FileService);
  
  onFileSelected(event: any) {
    const file: File = event.target.files[0];
    if (!file) {
      return;
    }
    this.fileService.uploadFile(file);
  }
}
