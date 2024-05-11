export declare interface User {
    id: string,
    firstName: string,
    lastName: string,
    roles: string,
    created: Date
}

export declare type UserList = Array<User>

export declare type UserListReducer =
    { type: "remove-users", payload: UserList }
    | { type: "display", payload: UserList }