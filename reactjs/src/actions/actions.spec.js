import { alertConstants, authConstants, portraitConstants } from '../constants';
import * as actions from './';
import { isConstructorDeclaration } from 'typescript';

describe('actions', () => {
    describe('alert actions', () => {
        it('success should create SUCCESS action', () => {
            expect( actions.alertActions.success  ( 'Use Redux is the message' ) ).toEqual({
                type: alertConstants.SUCCESS,
                message: 'Use Redux is the message'
            });
        });
        it('success should create ERROR action', () => {
            expect( actions.alertActions.error  ( 'Use Redux is the message' ) ).toEqual({
                type: alertConstants.ERROR,
                message: 'Use Redux is the message'
            });
        });
        it('success should create CLEAR action', () => {
            expect( actions.alertActions.clear  ( ) ).toEqual({
                type: alertConstants.CLEAR,
            });
        });
    });
});