import { HivemindPage } from './app.po';

describe('hivemind App', () => {
  let page: HivemindPage;

  beforeEach(() => {
    page = new HivemindPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
