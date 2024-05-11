import { PayloadAction, createSlice } from "@reduxjs/toolkit";
import { UserList, UserListReducer } from "../../model/props.ts";

const initialState: UserList = []

const userListSlice = createSlice({
    name: "user-list-slice",
    initialState,
    reducers: {
        userManagement: (state, action: PayloadAction<UserListReducer>) => {
            switch (action.payload.type) {
                case "display": {
                    state = [...state, ...action.payload.payload]
                    break;
                }
                case "remove-users": {
                    state = [...state, ...action.payload.payload]
                    break;
                }
                case "reset": {
                    state = initialState
                    break
                }
                default:
                    break
            }
        }
    }
})

export const { userManagement } = userListSlice.actions
export default userListSlice.reducer