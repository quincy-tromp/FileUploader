import { Component, signal, inject, ChangeDetectionStrategy } from '@angular/core';
import { FileUpload } from './file-upload/file-upload';
import { StoredFiles } from './stored-files/stored-files';
import { FileService } from './file-upload/file.service';

@Component({
  selector: 'app-root',
  imports: [FileUpload, StoredFiles],
  templateUrl: './app.html',
  styleUrl: './app.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class App {
  protected readonly title = signal('FileUploader.UI');
  private fileService = inject(FileService);
  protected storedFiles = this.fileService.storedFiles;

  constructor() {
    this.fileService.getStoredFiles();
  }

  
}
