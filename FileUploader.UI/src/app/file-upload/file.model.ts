export interface StoredFile {
  id: string;
  originalFileName: string;
  url: string;
  contentType?: string | null;
  fileSize?: number | null;
  uploadedAtUtc: Date;
}