export interface IPagination<Type> {
    pageNumber: number
    pageSize: number
    count: number
    data: Type[]
}