import { HttpClient, HttpResponse } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { Environment } from '../../environment';
import { StoredFile } from '../file-upload/file.model';

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

  downloadFile(fileId: string) {
  this.http.get(`${this.apiUrl}/files/${fileId}/download`, {
    responseType: 'blob' as const, 
    observe: 'response'
  }).subscribe({
    next: (response) => {
      const blob = response.body;
      
      let filename = this.getFileName(response) ?? 'downloaded-file';

      this.triggerDownload(blob as Blob, filename);
    },
    error: (error) => {
      console.error('Error downloading file:', error);
    }
  });
}

  private getFileName(response: HttpResponse<Blob>): string | null {
    const contentDisposition = response.headers.get('content-disposition');
    if (contentDisposition) {
      // Try to extract from filename*= (UTF-8 encoded)
      let match = contentDisposition.match(/filename\*=UTF-8''(.+?)(?:;|$)/);
      if (match) {
        return decodeURIComponent(match[1]);
      } else {
        // Fallback to filename= (standard)
        match = contentDisposition.match(/filename="?(.+?)"?(?:;|$)/);
        if (match) {
          return match[1];
        }
      }
    }
    return null;  
  }

  private triggerDownload(blob: Blob, filename: string): void {
    const url = URL.createObjectURL(blob);
    const anchor = document.createElement('a');
    anchor.href = url;
    anchor.download = filename;
    anchor.click();
    URL.revokeObjectURL(url);
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
