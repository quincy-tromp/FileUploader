import { Component, inject } from '@angular/core';
import { FileService } from '../shared/file.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-stored-files',
  imports: [DatePipe],
  templateUrl: './stored-files.html',
  styleUrl: './stored-files.css',
})
export class StoredFiles {
  private fileService = inject(FileService);
  protected files = this.fileService.storedFiles;

  downloadFile(fileId: string) {
    this.fileService.downloadFile(fileId);
  }
}
