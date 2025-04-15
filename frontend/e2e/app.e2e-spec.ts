import { SuperQATemplatePage } from './app.po';

describe('SuperQA App', function() {
  let page: SuperQATemplatePage;

  beforeEach(() => {
    page = new SuperQATemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
