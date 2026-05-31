import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { Environment } from '../../environment';
import { StoredFile } from './file.model';

@Injectable({
  providedIn: 'root',
})
export class FileService {
  private http = inject(HttpClient);
  
  private apiUrl = Environment.apiUrl;

  private _storedFiles = signal<StoredFile[]>([]);

  readonly storedFiles = this._storedFiles.asReadonly();

  uploadFile(file: File) {
    const formData = new FormData();
    formData.append('uploadedFile', file);

    return this.http.post(`${this.apiUrl}/files/upload`, formData).subscribe({
      next: (response) => {
        console.log('File uploaded successfully:', response);
        this.getStoredFiles();
      },
      error: (error) => {
        console.error('Error uploading file:', error);
      }
    });
  }

  getStoredFiles() {
    return this.http.get(`${this.apiUrl}/files`).subscribe({
      next: (response) => {
        this._storedFiles.set(response as StoredFile[]);
        console.log('Files retrieved successfully:', response);
      },
      error: (error) => {
        console.error('Error retrieving files:', error);
      }
    });
  }
}
