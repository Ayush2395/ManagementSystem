import { UserDto } from "../web-api-client"

export declare type UserList = Array<UserDto>

export declare type UserListReducer =
    { type: "remove-users", payload: UserList }
    | { type: "display", payload: UserList }
    | { type: "reset" }