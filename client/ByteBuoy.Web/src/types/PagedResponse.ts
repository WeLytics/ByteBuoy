import { PaginationMeta } from "./PaginationMeta";

export interface PagedResponse<T> {
    data: T;
    pagination?: PaginationMeta; 
}
