export interface ResponseResult<T> {
    isSuccessful: boolean,
    message: string,
    result: T
}