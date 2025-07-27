import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Post {
  id: number;
  title: string;
  content: string;
  authorId: number;
  authorName: string;
  createdAt: string;
  updatedAt?: string;
  viewCount: number;
  likeCount: number;
  commentCount: number;
  isPublished: boolean;
}

export interface PostDetail extends Post {
  comments: Comment[];
}

export interface Comment {
  id: number;
  content: string;
  postId: number;
  authorId: number;
  authorName: string;
  parentCommentId?: number;
  createdAt: string;
  updatedAt?: string;
  replies: Comment[];
}

export interface CreatePostRequest {
  title: string;
  content: string;
}

export interface UpdatePostRequest {
  title: string;
  content: string;
}

export interface CreateCommentRequest {
  content: string;
  postId: number;
  parentCommentId?: number;
}

export interface UpdateCommentRequest {
  content: string;
}

export interface PostsResponse {
  posts: Post[];
  totalCount: number;
  page: number;
  pageSize: number;
}

@Injectable({
  providedIn: 'root'
})
export class PostsService {
  private apiUrl = 'http://localhost:5106/api/posts';

  constructor(private http: HttpClient) { }

  getPosts(page: number = 1, pageSize: number = 10): Observable<Post[]> {
    return this.http.get<Post[]>(`${this.apiUrl}?page=${page}&pageSize=${pageSize}`);
  }

  getPost(id: number): Observable<PostDetail> {
    return this.http.get<PostDetail>(`${this.apiUrl}/${id}`);
  }

  createPost(request: CreatePostRequest): Observable<Post> {
    return this.http.post<Post>(this.apiUrl, request);
  }

  updatePost(id: number, request: UpdatePostRequest): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, request);
  }

  deletePost(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  getComments(postId: number): Observable<Comment[]> {
    return this.http.get<Comment[]>(`http://localhost:5106/api/comments/post/${postId}`);
  }

  createComment(request: CreateCommentRequest): Observable<Comment> {
    return this.http.post<Comment>('http://localhost:5106/api/comments', request);
  }

  updateComment(id: number, request: UpdateCommentRequest): Observable<void> {
    return this.http.put<void>(`http://localhost:5106/api/comments/${id}`, request);
  }

  deleteComment(id: number): Observable<void> {
    return this.http.delete<void>(`http://localhost:5106/api/comments/${id}`);
  }
} 