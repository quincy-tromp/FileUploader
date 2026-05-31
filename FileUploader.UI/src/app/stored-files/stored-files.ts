import { Component, inject } from '@angular/core';
import { FileService } from '../file-upload/file.service';
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
}
